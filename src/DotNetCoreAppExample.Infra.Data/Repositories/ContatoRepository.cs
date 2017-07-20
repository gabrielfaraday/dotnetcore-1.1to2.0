using System;
using System.Collections.Generic;
using DotNetCoreAppExample.Domain.Contatos.Entities;
using DotNetCoreAppExample.Domain.Contatos.Interfaces;
using Microsoft.EntityFrameworkCore;
using Dapper;
using System.Linq;
using DotNetCoreAppExample.Infra.Data.Context;

namespace DotNetCoreAppExample.Infra.Data.Repositories
{
    public class ContatoRepository : RepositoryBase<Contato>, IContatoRepository
    {
        public ContatoRepository(MainContext mainContext) : base(mainContext)
        {
        }

        public Endereco AdicionarEndereco(Endereco endereco)
        {
            return Db.Enderecos.Add(endereco).Entity;
        }

        public Telefone AdicionarTelefone(Telefone telefone)
        {
            return Db.Telefones.Add(telefone).Entity;
        }

        public Endereco AtualizarEndereco(Endereco endereco)
        {
            return Db.Enderecos.Update(endereco).Entity;
        }

        public Telefone AtualizarTelefone(Telefone telefone)
        {
            return Db.Telefones.Update(telefone).Entity;
        }

        public ICollection<Contato> ObterContatosAtivos()
        {
            var sql = @"SELECT * 
                          FROM CONTATOS
                         WHERE ATIVO = 1
                      ORDER BY NOME ";

            return Db.Database.GetDbConnection().Query<Contato>(sql).AsList();
        }

        public Endereco ObterEnderecoPorId(Guid enderecoId)
        {
            var sql = @"SELECT * FROM Enderecos WHERE Id = @pid";

            var endereco = Db.Database.GetDbConnection().Query<Endereco>(sql, new { pid = enderecoId });

            return endereco.SingleOrDefault();
        }

        public Contato ObterContatoPorEmail(string email)
        {
            var sql = @"SELECT * FROM Contatos WHERE email = @pemail";

            var endereco = Db.Database.GetDbConnection().Query<Contato>(sql, new { pemail = email });

            return endereco.SingleOrDefault();
        }

        public Telefone ObterTelefonePorId(Guid telefoneId)
        {
            var sql = @"SELECT * FROM Telefones WHERE Id = @pid";

            var telefone = Db.Database.GetDbConnection().Query<Telefone>(sql, new { pid = telefoneId });

            return telefone.SingleOrDefault();
        }

        public void RemoverTelefone(Guid telefoneId)
        {
            Db.Telefones.Remove(ObterTelefonePorId(telefoneId));
        }

        #region [ Overrides ]

        public override Contato FindById(Guid id)
        {
            var sql = @"SELECT *
                          FROM Contatos C
                     LEFT JOIN Telefones T ON C.Id = T.ContatoId
                     LEFT JOIN Enderecos E ON C.Id = E.ContatoId
                      WHERE C.Id = @pid";

            var evento = Db.Database.GetDbConnection().Query<Contato, Telefone, Endereco, Contato>(sql,
                (c, t, e) =>
                {
                    if (t != null)
                        c.AtribuirTelefone(t);

                    if (e != null)
                        c.AtribuirEndereco(e);

                    return c;
                }, new { pid = id });

            return evento.FirstOrDefault();
        }

        public override void Delete(Guid id)
        {
            var contato = FindById(id);
            contato.RemoverContato();
            Update(contato);
        }

        #endregion [ Overrides ]
    }
}
