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
    
    public partial class Notification
    {
        public int IdNotification { get; set; }
        public int IdTypeNotification { get; set; }
        public string TitleNotification { get; set; }
        public string DescriptionNotification { get; set; }
        public bool CheckIt { get; set; }
        public int IdSender { get; set; }
        public int IdRecipient { get; set; }
    
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual TypeNotification TypeNotification { get; set; }
    }
}
