﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetSport.Dto.Roles
{
    public class AddClaimRoleDto
    {
        [Required]
        public ulong Id { get; set; }
    }
}
