using System;

namespace Urbagestion.DataAccess
{
    public class JoinTable
    {
        public JoinTable(Type sideA, Type sideB)
        {
            SideA = sideA;
            SideB = sideB;
        }

        public Type SideA { get; }

        public Type SideB { get; }
    }
}