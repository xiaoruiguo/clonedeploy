﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CloneDeploy_Entities
{
    [Table("boot_menu_templates")]
    public class BootTemplateEntity
    {
        [Column("boot_menu_template_contents", Order = 4)]
        public string Contents { get; set; }

        [Column("boot_menu_template_description", Order = 3)]
        public string Description { get; set; }

        [Column("boot_menu_template_id", Order = 1)]
        public int Id { get; set; }

        [Column("boot_menu_template_name", Order = 2)]
        public string Name { get; set; }
    }
}