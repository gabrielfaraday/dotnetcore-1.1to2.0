using FluentValidation;
using FluentValidation.Results;
using System;

namespace DotNetCoreAppExample.Domain.Core
{
    public abstract class EntityBase<T> : AbstractValidator<T> where T : EntityBase<T>
    {
        protected EntityBase()
        {
            ValidationResult = new ValidationResult();
        }

        public Guid Id { get; protected set; }
        public ValidationResult ValidationResult { get; protected set; }

        public abstract bool EstaValido();

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase<T>;

            if (ReferenceEquals(this, compareTo)) return true;
            if (ReferenceEquals(null, compareTo)) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(EntityBase<T> a, EntityBase<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase<T> a, EntityBase<T> b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 900) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return $"{GetType().Name} - Id = {Id}";
        }
    }
}
