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
    using System.Linq;

    public partial class Выставка
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Выставка()
        {
            this.Билет = new HashSet<Билет>();
            this.ЭкспонатВыставка = new HashSet<ЭкспонатВыставка>();
        }
    
        public int Код { get; set; }
        public string Название { get; set; }
        public System.DateTime ВремяНачала { get; set; }
        public System.DateTime ВремяОкончания { get; set; }
        public decimal ЦенаБилета { get; set; }
        public string Описание { get; set; }
        public int МаксимумПосетителей { get; set; }
        public int БилетовКуплено { get { using (var context = new MusCassaEntities()) { return context.Билет.Where(a => a.Выставка.Equals(this.Код)).Count(); } } }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Билет> Билет { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ЭкспонатВыставка> ЭкспонатВыставка { get; set; }
    }
}
