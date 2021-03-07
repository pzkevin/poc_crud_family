using System;
namespace POC_CRUDFamily.App
{
    public class SearchCollection
    {
        //ATTIRBS
        public string Name { get; set; }
        public CriteriaOperator Operator { get; set; }
        public string Value { get; set; } //34.99  '2021-21-21'
        public bool IsVarchar { get; set; } //34.99  '2021-21-21'
        public LogicOperator LogicOp { get; set; }

       

        public SearchCollection(string name, CriteriaOperator @operator, string value, bool isVarchar, LogicOperator logicOp)
        {
            Name = name;
            Operator = @operator;
            Value = value;
            IsVarchar = isVarchar;
            LogicOp = logicOp;
        }
    }
}
