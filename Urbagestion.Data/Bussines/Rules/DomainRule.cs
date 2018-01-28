using System;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Bussines.Rules
{
    public abstract class DomainRule<T> where T : Entity
    {
        protected DomainRule(Func<T, bool> rule, string errorMessage)
        {
            Rule = rule;
            ErrorMessage = errorMessage;
        }


        public Func<T, bool> Rule { get; }

        public string ErrorMessage { get; }
    }
}