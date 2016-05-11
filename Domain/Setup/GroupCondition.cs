using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string MinimumAmountOfOccurences { get; set; }
        public string MaximumAmountOfOccurences { get; set; }
        public string ParentGroup { get; set; }

        [Required]
        public FileSpecification FileSpecification { get; set; }
        public ICollection<FileSpecFieldCondition> FileSpecFieldConditions { get; set; }
    }
}
