//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _321_Khitrinovich_2UP
{
    using System;
    using System.Collections.Generic;
    
    public partial class Workers
    {
        public int ID { get; set; }
        public string FIO { get; set; }
        public string Password { get; set; }
        public Nullable<int> RoleId { get; set; }
    
        public virtual Roles Roles { get; set; }
    }
}
