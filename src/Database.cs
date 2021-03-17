using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Forms.Firebase.src
{
    public class Database<T>
    {
        /// <summary>
        /// Hold Table Names
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// Hold collection of Type T
        /// </summary>
        public List<T> Collection { get; set; }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="tableName">passed table name</param>
        public Database(string tableName=null)
        {
            //initialize attributes
            TableName = tableName;
            Collection = new List<T>();

            //Execute Initialization
            Init();
        }

        /// <summary>
        /// Initialization
        /// </summary>
        private async void Init()
        {
            try
            {
                //get collection from database
                var list = await GlobalInstance.DatabaseClient.Child(TableName).OnceAsync<T>();

                //fill collection
                foreach (var item in list)
                {                    
                    Collection.Add(item.Object);
                }
            }
            catch (Exception)
            {

                Collection = new List<T>();
            }
        }

        /// <summary>
        /// checks the objects in the database
        /// </summary>
        /// <param name="obj">object to be checked</param>
        /// <returns></returns>
        public async Task<bool> InstanceExist(T obj)
        {
            try
            {
                //get collection from database
                var list = await GlobalInstance.DatabaseClient.Child(TableName).OnceAsync<T>();
                
                //search for the object
                var res = list.Where(u => u.Object.Equals(obj)).FirstOrDefault();

                //return result
                if (res.Object == null)
                    return await Task.FromResult(false);
                else
                    return await Task.FromResult(true);
            }
            catch (Exception)
            {
                //return false
                return await Task.FromResult(false);
            }
        }

    }
}
