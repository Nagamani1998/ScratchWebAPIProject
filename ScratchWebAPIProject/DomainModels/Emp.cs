using System;
using System.Collections.Generic;

#nullable disable

namespace ScratchWebAPIProject.DomainModels
{
    public partial class Emp
    {
        public short Empno { get; set; }
        public string Ename { get; set; }
        public string Job { get; set; }
        public short? Mgr { get; set; }
        public DateTime? Hiredate { get; set; }
        public decimal? Sal { get; set; }
        public decimal? Comm { get; set; }
        public short? Deptno { get; set; }

        public virtual Dept DeptnoNavigation { get; set; }
    }
}
