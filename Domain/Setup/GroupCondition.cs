﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIS.BL.Domain.Setup
{
    public class GroupCondition
    {
        /*Contains validation information for one specific type of group*/

        public int GroupConditionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Level { get; set; }
        public int MinimumAmountOfOccurences { get; set; }
        public int MaximumAmountOfOccurences { get; set; }
        public int TransactionNumber { get; set; }
        public FileSpecification FileSpecification { get; set; }

    }
}