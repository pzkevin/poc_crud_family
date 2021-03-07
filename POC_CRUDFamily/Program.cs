using System;
using System.Collections.Generic;
using POC_CRUDFamily.App;

namespace POC_CRUDFamily
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("conectando y haciendo INSERT a SQLSERVER");
            //instancia de CRUD
            CRUD crud = new CRUD();
            //creamos lista de DataCollection
            //con estos datos:
            //'lata coca cola','lata coca cola 300ml sin azucar', 20.50, '7505484542517'
            List<DataCollection> datosParainsert = new List<DataCollection>();

            datosParainsert.Add(new DataCollection("name", Types.NVARCHAR, "Doritos Nachos 300gr"));
            datosParainsert.Add(new DataCollection("description", Types.NVARCHAR, "bolsa de doritos nachos de 300gr"));
            datosParainsert.Add(new DataCollection("price", Types.MONEY, 77.66));
            datosParainsert.Add(new DataCollection("bar_code", Types.NVARCHAR, "098098098098"));

            //ejecutar el iinsert mediante el metodo CREATE
            bool resDeInsertar = crud.create(datosParainsert);

            if (resDeInsertar)
                Console.WriteLine("Si se insertó correctamente el registro");
            else
                Console.WriteLine($"ERROR {CRUD.ERROR}, no se insertó correctamente el registro");



            //'lata coca cola','lata coca cola 300ml sin azucar', 20.50, '7505484542517'
            List<DataCollection> datosParaUpdate = new List<DataCollection>();

            datosParaUpdate.Add(new DataCollection("name", Types.NVARCHAR, ""));
            datosParaUpdate.Add(new DataCollection("description", Types.NVARCHAR, ""));
            datosParaUpdate.Add(new DataCollection("price", Types.MONEY, 0));
            datosParaUpdate.Add(new DataCollection("bar_code", Types.NVARCHAR, ""));

            //ejecutar el iinsert mediante el metodo CREATE
            bool resDeUpdate = crud.update(datosParaUpdate,3);

            if (resDeInsertar)
                Console.WriteLine("Si se modificó correctamente el registro");
            else
                Console.WriteLine($"ERROR {CRUD.ERROR}, no se modificó correctamente el registro");

            ////////////////////////////////////////////////////////
            ///borrar
            if(crud.delete(3)) 
                Console.WriteLine("Si se borró correctamente el registro");
            else
                Console.WriteLine($"ERROR {CRUD.ERROR}, no se borró correctamente el registro");


            ////////////////////////
            ///consultar
            List<SearchCollection> busqueda = new List<SearchCollection>();
            busqueda.Add(new SearchCollection("name", CriteriaOperator.EQUALS, "Vita 100ml",true,  LogicOperator.NADA));

            List<object> resultados  = crud.read(busqueda);

            foreach (object registro in resultados)
            {
                //hacemos casting entre object y string[]
                string[] registroResultante = (string[])registro;
                //imprimir los datos
                foreach (string campo in registroResultante)
                {
                    Console.WriteLine("Campo= "+campo);
                }
                Console.WriteLine("registro---------------------------------------------");
            }



            Console.WriteLine("cualquer tecla para finalizar");
            Console.ReadKey();

        }
    }
}
