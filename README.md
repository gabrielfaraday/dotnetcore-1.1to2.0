#### ATENÇÃO: NÃO ABRA NENHUMA SOLUTION NO VS ATÉ QUE SOLICITADO MAIS ABAIXO.

1- Clone a ultima versão da aplicação deste repositório para sua máquina. 

2- Clone o repositório vazio da nova aplicação que você deseja criar.

3- Copie o conteúdo clonado no passo 1 para a pasta vazia do passo 2, EXCETO O ARQUIVO README.MD e AS PASTAS .GIT, .VS E PACKAGES. Estes são especificos por projeto.

4- Abra o Notepad++. Caso não tenho instalado baixe ultima versão de https://notepad-plus-plus.org/download/

	a- Pressione Ctrl+Shift+F e selecione a aba "Find in Files".
	
	b- No campo "Find what:" informe o texto que queremos buscar para substituir, que é "DotNetCoreAppExample" (sempre sem as aspas).
	
	c- No campo "Replace with:" informe o texto para utilizar na substituição.
	
	Por padrão devemos sempre usar o nome do cliente/empresa + '.' + nome do projeto/sistema. Por exemplo: "RedeXYZ.PortalDeNoticias" ou "ABC.ProjetoX"
	
	d- No campos "Filters:" informe *.*
	
	e- No campo directory informe o caminho da pasta clonada do seu novo projeto, clonada no passo 2 acima.
	
	f- Certifique-se de que os checkboxes "Match whole word only" e "Match case" NÃO ESTEJAM selecionados.
	
	g- Certifique-se de que os checkboxes "In all sub-folders" e "In hidden fields" ESTEJAM selecionados.
	
	h- Clique no botão "Replace in Files".
	
	i- O Notepad++ exibirá um alerta questionando se deseja realmente prosseguir. Clique em OK.
	
	j- Aguarde ele fazer as substituições. Fique tranquilo, é esperado mais de 4.800 alterações!

5- Altere o nome da solution de seu projeto conforme item 4.c acima. Por exemplo "DotNetCoreAppExample.sln" vira "ABC.ProjetoX.sln"

6- Abra as pastas "src/" e "tests/" e altere o NOME DAS PASTAS substituindo "DotNetCoreAppExample" pelo nome da sua aplicação conforme item 4.c acima. Por exemplo "DotNetCoreAppExample.Domain" vira "ABC.ProjetoX.Domain", etc.

7- Acesse cada pasta renomeada no item 6 acima e RENOMEIE OS ARQUIVOS .CSPROJ conforme item 4.c acima. Por exemplo "DotNetCoreAppExample.Domain.csproj" vira "ABC.ProjetoX.Domain.csproj", etc.
	
	a- Se em alguma das pastas houver um arquivo "*.csproj.user" renomeie-o também seguindo mesma regra (projetos web/api costumam ter).
	
8- Se houver algum arquivo na pasta App_Data do projeto web pode remover.

9- ABRA A SOLUTION NO VS DESEJADO! (A PARTIR DO VS 2017)

10- No VS clique com botão direito do mouse sobre a Solution e selecione "Clean Solution".

11- No VS clique com botão direito do mouse sobre a Solution e selecione "Rebuild Solution". Tudo deverá estar buildando corretamente neste ponto!

12- No VS clique com botão direito do mouse sobre o projeto web e selecione "Set as StartUp Project".

13- Pressione F5. O site deve subir sem nenhum erro/problema.

OBS: para funcionar a integração com o banco, deve ser verificado as connection strings e adicionado/rodado as migrations.

#### PARABÉNS! AGORA É SÓ CODAR! :)