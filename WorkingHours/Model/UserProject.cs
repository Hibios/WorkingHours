//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorkingHours.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserProject
    {
        public int IdUserProject { get; set; }
        public int IdUser { get; set; }
        public int IdProject { get; set; }
    
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
    }
}
