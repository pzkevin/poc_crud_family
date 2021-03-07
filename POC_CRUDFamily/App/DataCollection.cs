using System;
namespace POC_CRUDFamily.App
{
    public class DataCollection
    {
        //atribb
        public string Name { get; set; }
        public Types Type { get; set; }
        public object Value { get; set; }


        /// <summary>
        /// DataCollection me permite representar cada CAMPO en un QUERY
        /// Que queremos ejecutar con CRUD.
        /// </summary>
        /// <param name="name">Nombre del CAMPO en la BD</param>
        /// <param name="type">Tipo del campo, que esté en el enum Types</param>
        /// <param name="value"></param>
        public DataCollection(string name, Types type, object value)
        {
            Name = name;
            Type = type;
            Value = value;
        }
    }
}
