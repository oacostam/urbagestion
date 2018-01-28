using System.Collections.Generic;
using Urbagestion.Model.Common;

namespace Urbagestion.Model.Bussines.Rules
{
    public static class EntityValidator<T> where T : Entity
    {
        private static readonly List<DomainRule<T>> DomainRules;

        static EntityValidator()
        {
            DomainRules = new List<DomainRule<T>>();
        }

        public static void AddRule(DomainRule<T> rule)
        {
            DomainRules.Add(rule);
        }

        public static IEnumerable<string> Validate(T value)
        {
            foreach (var domainRule in DomainRules)
                if (!domainRule.Rule.Invoke(value))
                    yield return domainRule.ErrorMessage;
        }
    }
}