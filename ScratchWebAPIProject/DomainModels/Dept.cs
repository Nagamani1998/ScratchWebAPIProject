using System;
using System.Collections.Generic;

#nullable disable

namespace ScratchWebAPIProject.DomainModels
{
    public partial class Dept
    {
        public Dept()
        {
            Emps = new HashSet<Emp>();
        }

        public short Deptno { get; set; }
        public string Dname { get; set; }
        public string Loc { get; set; }

        public virtual ICollection<Emp> Emps { get; set; }
    }
}
