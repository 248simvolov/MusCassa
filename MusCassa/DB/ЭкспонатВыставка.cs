//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MusCassa.DB
{
    using System;
    using System.Collections.Generic;
    
    public partial class ЭкспонатВыставка
    {
        public int Код { get; set; }
        public int Выставка { get; set; }
        public int Эксопнат { get; set; }
    
        public virtual Выставка Выставка1 { get; set; }
        public virtual Экспонат Экспонат { get; set; }

        public static explicit operator ЭкспонатВыставка(List<ЭкспонатВыставка> v)
        {
            throw new NotImplementedException();
        }
    }
}
