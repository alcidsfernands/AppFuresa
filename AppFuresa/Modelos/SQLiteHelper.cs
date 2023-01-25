using System;
using System.Collections.Generic;
using System.Text;
using SQLite;    //importar libraria Sqlite
//using FURESCREEN.Modelos;   //para tener aceso a las clases en la carpeta model
using System.Threading.Tasks;

namespace AppFuresa.Modelos
{
    public class SQLiteHelper
    {
        public SQLiteAsyncConnection db;     //conexion con la base de datos db

        public SQLiteHelper(string dbPath) // metodo construtor
        {
            try
            {
                db = new SQLiteAsyncConnection(dbPath);
                db.CreateTableAsync<user>().Wait();   //crear una tabla con los atributos del modelo user
                db.CreateTableAsync<Servidores>().Wait();

            }
            catch (Exception ex)
            {
              
                throw;
            }
        }

        // crear una tarea para guardar o registros en la tabla

        public Task<int> guardaUserAsync(user USER)
        {
            if (USER.Username != string.Empty)
            {
                return db.InsertAsync(USER);
            }
            else
                return null;
        } 
        public Task<List<T>> getUserAsync<T>() where T:new()   //metodo para buscar todas filas de la tabla user
        {
            return db.Table<T>().ToListAsync();      
        }
        public Task<user> getUserAsysnc(string username)  //metodo para buscar solo un elemento
        {
            return db.Table<user>().Where(a => a.Username==username).FirstOrDefaultAsync();
        }
        public Task<int> BorrarUserAll()
        { 
            return db.DropTableAsync<user>();
        }



        public Task<int> guardaServerAsync(Servidores server)
        {
            if (server.Username != string.Empty)
            {
                return db.InsertAsync(server);
            }
            else
                return null;
        }
        public Task<List<T>> getServerAsync<T>() where T : new()   //metodo para buscar todas filas de la tabla user
        {
            return db.Table<T>().ToListAsync();
        }

        public Task<int> BorrarServerAll()
        {
            return db.DropTableAsync<Servidores>();
        }


        public Task<int> DeleteItemServerAsync(Servidores ser)
        {
            return db.DeleteAsync(ser);
        }



    }
}
