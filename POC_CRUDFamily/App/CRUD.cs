using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;

namespace POC_CRUDFamily.App
{
    public  class CRUD
    {
        //atttribs
        public string Table { get; set; }
        public List<DataCollection> fieldList = new List<DataCollection>();
        public static string ERROR;

        //construct dfault
        public CRUD()
        {
        }
        //constructor sobrecargado que acepta la TABLA
        public CRUD(string table)
        {
            this.Table = table;
        }

        //métodos concretos
        public bool create(List<DataCollection> data)
        {
            bool res = false;
            SqlConnection con = new SqlConnection();
            //trabajar con la base de datos, para hacer un insert
            //INSERT INTO tabla (c1, c2, c3..cn) VALUES (v1, v2, v3, ... vN);

            //PROCEDIMIENTO DE ACCION DE BD
            try
            {
                //creamos el conexion string
                string conString = "Server=127.0.0.1;Database=dbprueba;User Id=SA;Password=SqlServer2017;";
                //abrimos conexion
                con=new SqlConnection(conString);
                con.Open();
                //establecemos el command
                SqlCommand com = new SqlCommand();
                //ponemos el texto del query
                com.CommandText = " INSERT INTO products (";
                //generar un string con tgodos los campos separados por ','
                foreach (DataCollection dato in data)
                    com.CommandText += dato.Name + ",";
                //retiramos la ultima coma
                        com.CommandText = com.CommandText.Remove(com.CommandText.Length-1);
                //Continuamos con la contactenacion
                com.CommandText += " ) VALUES (";
                //generar un string con tgodos los VALUES separados por ','
                /// YYYYY con sus respectivos apostrofes en textos y fechas
                for (int i = 0; i < data.Count; i++)
                {
                    if (data[i].Type == Types.DATE || data[i].Type == Types.DATETIME || data[i].Type == Types.NTEXT || data[i].Type == Types.NVARCHAR || data[i].Type == Types.TEXT || data[i].Type == Types.TIME || data[i].Type == Types.VARCHAR)                
                        com.CommandText += $"'{data[i].Value}',";
                    else
                        com.CommandText += $"{data[i].Value},";
                
                }
                //retiramos la ultima coma
                com.CommandText = com.CommandText.Remove(com.CommandText.Length - 1);
                //se cierra el QUERY 
                com.CommandText += " );";
                
                //asociamos el command al CONEXION
                com.Connection = con;
                //executamos el command
                com.ExecuteNonQuery();
                //devolvemos true
                res = true;
                
            }
            catch (SqlException sqlex)
            {
                CRUD.ERROR = "ERROR SQL AL INSERTAR. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                CRUD.ERROR = "ERROR de Comunicación AL INSERTAR. " + ioex.Message;
            }
            catch (Exception ex)
            {
                CRUD.ERROR = "ERROR general AL INSERTAR. " + ex.Message;
            }
            finally
            {
                //se ejcuta siempre, aunque no haya error
                //cerramos l a conexion
                con.Close();
            }
          
            //retornamos true si se ejecuta crroectamente el try, false cualqueir otro caso
            return res;
        }


        public bool createMySql(List<DataCollection> data)
        {
            bool res = false;
            MySqlConnection con= new MySqlConnection();
            //trabajar con la base de datos, para hacer un insert
            //INSERT INTO tabla (c1, c2, c3..cn) VALUES (v1, v2, v3, ... vN);

            //PROCEDIMIENTO DE ACCION DE BD
            try
            {
                //creamos el conexion string
             
                string conString = "Server=127.0.0.1;Port=8889; Database=dbprueba;Uid=root;Pwd=root;";
                //abrimos conexion
                con = new MySqlConnection(conString);
                con.Open();
                //establecemos el command
                MySqlCommand com = new MySqlCommand();
                //ponemos el texto del query
                //com.Com⁄mandText = $"INSERT INTO products (name, description, price, bar_code) VALUES ('{data[0].Value}','{data[1].Value}', {data[2].Value}, '{data[3].Value}');";
                com.CommandText = $"INSERT INTO products (";
                foreach (DataCollection dato in data)
                {
                    com.CommandText += dato.Name + ",";
                }
                com.CommandText += ") VALUES (";
                foreach (DataCollection dato in data)
                {
                    com.CommandText += dato.Value + ",";
                }
                com.CommandText += ");";
                //asociamos el command al CONEXION
                com.Connection = con;
                //executamos el command
                com.ExecuteNonQuery();
                //devolvemos true
                res = true;
              
            }
            catch (MySqlException sqlex)
            {
                CRUD.ERROR = "ERROR SQL AL INSERTAR. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                CRUD.ERROR = "ERROR de Comunicación AL INSERTAR. " + ioex.Message;
            }
            catch (Exception ex)
            {
                CRUD.ERROR = "ERROR general AL INSERTAR. " + ex.Message;
            }
            finally
            {
                //se ejcuta siempre, aunque no haya error
                //cerramos l a conexion
                con.Close();
            }

            //retornamos true si se ejecuta crroectamente el try, false cualqueir otro caso
            return res;
        }

        public bool update(List<DataCollection> data, int id)
        {
            //var de resultado
            bool res = false;
            //vars que necesitamos
            SqlConnection con = new SqlConnection();
            SqlCommand com = new SqlCommand();
            //bloque de intrucciones try catch
            try
            {
                //generar connectionstring
                string connString = "Server=localhost;Database=dbprueba;User Id=SA;Password=SqlServer2017;";
                //instancia conex con connectionstring
                con = new SqlConnection(connString);
                //abrir
                con.Open();
                //generar command con el QUERY - update
                //com.CommandText = "UPDATE products SET c1 = v1, c2=v2... cn=vn, WHERE id=1"                //
                com.CommandText = "UPDATE products SET ";
                foreach (DataCollection dato in data)
                {
                    //validar si el Type necesita apostrofes
                    if (dato.Type == Types.DATE || dato.Type == Types.DATETIME || dato.Type == Types.NTEXT || dato.Type == Types.NVARCHAR || dato.Type == Types.TEXT || dato.Type == Types.TIME || dato.Type == Types.VARCHAR)
                        com.CommandText += $"{dato.Name} = '{dato.Value}',";
                    else
                        com.CommandText += $"{dato.Name} = {dato.Value},";
                }
                //quitamos la coma final ','
                com.CommandText = com.CommandText.Remove(com.CommandText.Length - 1);
                //concatenamos el resto de la sentencia UPDATE
                com.CommandText += $" WHERE id={id}";
                //asociar el con al command
                com.Connection = con;
                //ejecutar el COMMAND  - DDL, DML => executeNonQuery, SELECT => executeReader, SCALAR => executeScalar
                int rowsAffected = com.ExecuteNonQuery();
                //camb iamos el res=true
                if (rowsAffected == 1)
                    res = true;

            }
            catch (SqlException sqlex)
            {
                CRUD.ERROR = "ERROR SQL en actualizar. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                CRUD.ERROR = "ERROR de I/O en actualizar. " + ioex.Message;
            }
            catch (Exception ex)
            {
                CRUD.ERROR = "ERROR general, no identificado, al Actualizar. " + ex.Message;
            }
            finally
            {
                //cerrar conexion truene o llueve
                if(con.State != ConnectionState.Closed)
                    con.Close();
            }
            //retornamos lo que quede
            return res;
        }

        public bool delete(int id)
        {
            //var de resultado
            bool res = false;
            //vars para acciones de BD
            SqlConnection con = new SqlConnection();
            SqlCommand com = new SqlCommand();
            try
            {
                //crear el connString
                string connString = "Server=127.0.0.1;Database=dbprueba;User Id=SA;Password=SqlServer2017;";
                //instancias ela con incluyendo connString
                con = new SqlConnection(connString);
                //abrir
                if (con.State == ConnectionState.Broken)
                {
                    con.Close(); con.Open();
                }else if(con.State == ConnectionState.Closed)
                    con.Open();

                //genera el QUERY
                com.CommandText = $"DELETE FROM products WHERE id = {id}";
                //asociar el connetion al command
                com.Connection = con;
                //ejecutar el query
                int rowsBorrados = com.ExecuteNonQuery();
                //cambiar el res a true
                if (rowsBorrados == 1)
                    res = true;
                else
                    CRUD.ERROR = $"El registro con el id={id}, no existe. ";
            }
            catch (SqlException sqlex)
            {
                CRUD.ERROR = "ERROR SQL al borrar. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                CRUD.ERROR = "ERROR I/O al borrar. " + ioex.Message;
            }
            catch (Exception ex)
            {
                CRUD.ERROR = "ERROR gral, no especificado, al borrar. " + ex.Message;

            }
            finally
            {
                con.Close();
            }
            //retornamos el res
            return res;
        }

        public List<object> read(List<SearchCollection> search)
        {
            //creamos la lista que sera el recipiente de los res de la consulta
            List<object> res = new List<object>();
            //vars de acciones de bbd
            SqlConnection con = new SqlConnection();
            SqlCommand com = new SqlCommand();
            //SqlDataReader reader;
            //try 
            try
            {
                //connString
                string connString = "Server=localhost;Database=dbprueba;User Id=SA;Password=SqlServer2017;";
                //crear conex
                con = new SqlConnection(connString);
                //abrir conex
                con.Open();
                //crear el query
                com.CommandText = "SELECT * FROM products WHERE  ";
                com.Connection = con;
                //filtrado de serch collection para los criterios de busquda
                foreach (SearchCollection criterio in search)
                {
                    // campo OP 'valor' AND|OR|' '
                    if (criterio.IsVarchar) //         description   LIKE    "%TKT%"                
                        com.CommandText += $" {criterio.Name} {parseSearchOp(criterio.Operator)} '{criterio.Value}' {(criterio.LogicOp== LogicOperator.NADA?"":criterio.LogicOp)} ";
                    else
                        com.CommandText += $" {criterio.Name} {parseSearchOp(criterio.Operator)} {criterio.Value} {criterio.LogicOp} ";
                }
                //ejecutar el query y recibir en reader
                SqlDataReader reader = com.ExecuteReader();
                //validar los resultados
                if (reader.HasRows)
                {
                    //si hay campos que leer
                    if (reader.FieldCount > 0)
                    {
                        //leemos el REGISTRO ya que SI es vñalido
                        while (reader.Read())
                        {
                            //arreglo para recibir los datos del REGISTRO LEIDO
                            int size = reader.FieldCount;
                            string[] registro =
                            {
                                reader.GetInt32(0).ToString(),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetDouble(3).ToString(),
                                reader.GetString(4)
                            };
                            //for (int i = 0; i < reader.FieldCount; i++)
                            //   registro[i] = reader.GetValue(i).ToString();

                            //agregamos a la lista de resultdos
                            res.Add(registro);
                        }
                       
                    }
                    else
                    {
                        //regitro invalido
                        CRUD.ERROR = "El registro no tiene CAMPOS válidos que leer.";
                    }
                    //mientras se puedan leer registros del reader, los procesamos, REGISTRO POR REGISTRO
                }
                else
                {
                    CRUD.ERROR = $"La consulta -{com.CommandText}-, no arroja ningún resultado";
                }
                //conformar resultados para el retorno
                //cerrar el reader
            }
            catch (SqlException sqlex)
            {
                CRUD.ERROR = "Error SQL al consultar. " + sqlex.Message;
            }
            catch (IOException ioex)
            {
                CRUD.ERROR = "Error I/O al consultar. " + ioex.Message;
            }
            catch (Exception ex)
            {
                CRUD.ERROR = "Error gral al consultar. " + ex.Message;
            }
            finally
            {
                con.Close();
            }
            //retornmaso los resultados de la consulta
            return res;
        }

        private string parseSearchOp(CriteriaOperator op)
        {
            string res = "";
            switch (op)
            {
                case CriteriaOperator.EQUALS:
                    res = "=";
                    break;

                case CriteriaOperator.NOT_EQUALS:
                    res = "!="; // ! =
                    break;
                case CriteriaOperator.GREATER_THAN:
                    res = ">"; // 
                    break;
                case CriteriaOperator.GREATER_THAN_EQUALS:
                    res = ">="; // > =
                    break;
                case CriteriaOperator.LESS_THAN:
                    res = "<"; // 
                    break;
                case CriteriaOperator.LESS_THAN_EQUALS:
                    res = "<="; // < =
                    break;

                case CriteriaOperator.LIKE:
                    res = " LIKE "; // < =
                    break;
                default:
                    res = "";
                    break;

            }
            return res;
        }
    }
}
