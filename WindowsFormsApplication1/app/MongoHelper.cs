using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;  

namespace WindowsFormsApplication1.app
{
    public class MachineStatus {
        private static string tableName = "machineStatus";
        public MachineStatus() { }
        public String machineID { get; set; }
        public String lastStatus { get; set; }
        public String startTime { get; set; }
        public String endTime { get; set; }

        public Boolean runningFlag { get;set; }

        public MachineStatus(String machineID, String lastStatus, String startTime, String endTime,Boolean runningFlag)
        {
            this.machineID = machineID;
            this.lastStatus = lastStatus;
            this.startTime = startTime;
            this.endTime = endTime;
            this.runningFlag = runningFlag;
        }
        public Boolean Insert()
        {
            BsonDocument dom = new BsonDocument {  
                { "machineID", machineID },  
                { "lastStatus", lastStatus },  
                {"startTime",startTime},
                {"endTime",endTime},
                {"runningFlag",runningFlag}
            };
            return MongoHelper.Insert(tableName, dom);
        }
        public static IEnumerable<MachineStatus> Search(IMongoQuery query)
        {
            foreach (BsonDocument tmp in MongoHelper.Search(tableName, query))
                yield return new MachineStatus(tmp["machineID"].AsString, tmp["lastStatus"].AsString, tmp["startTime"].AsString, tmp["endTime"].AsString, tmp["runningFlag"].AsBoolean);
        }
        public static Boolean Remove(IMongoQuery query)
        {
            return MongoHelper.Remove(tableName, query);
        }
        public static Boolean Update(IMongoQuery query, IMongoUpdate new_doc, UpdateFlags flags)
        {
            return MongoHelper.Update(tableName, query, new_doc, flags);
        }

    }
    public class Users
    {
        private static string tableUser = "Users";

        public Users() { }
        public Users(String name, Int32 age, String sex)
        {
            Name = name;
            Age = age;
            Sex = sex;
        }
        public String Name { get; set; }
        public Int32 Age { get; set; }
        public String Sex { get; set; }
        public Boolean Insert()
        {
            BsonDocument dom = new BsonDocument {  
                { "name", Name },  
                { "age", Age },  
                {"sex",Sex}  
            };
            return MongoHelper.Insert(tableUser, dom);
        }
        public static IEnumerable<Users> Search(IMongoQuery query)
        {
            foreach (BsonDocument tmp in MongoHelper.Search(tableUser, query))
                yield return new Users(tmp["name"].AsString, tmp["age"].AsInt32, tmp["sex"].AsString);
        }
        public static Boolean Remove(IMongoQuery query)
        {
            return MongoHelper.Remove(tableUser, query);
        }
        public static Boolean Update(IMongoQuery query, IMongoUpdate new_doc, UpdateFlags flags)
        {
            return MongoHelper.Update(tableUser, query, new_doc, flags);
        }
    }
    public  class MongoHelper
    {
        public static string connectionString = "mongodb://112.124.23.181";
        //数据库名  
        private static string databaseName = "IMDB";
         /// <summary>  
        /// 查询排序
        /// </summary>  
        public static FindAndModifyResult FindAndModify(String collectionName,IMongoQuery query, IMongoSortBy sortBy, IMongoUpdate update, bool returnNew, bool upsert)
        {
                    //定义Mongo服务  
            MongoServer server = MongoServer.Create(connectionString);
            //获取databaseName对应的数据库，不存在则自动创建  
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);


                return collection.FindAndModify(
                    query,
                    sortBy,
                    update,
                    true,
                    true
                );
        }
        public static MongoCursor<BsonDocument> Search(String collectionName, IMongoQuery query)
        {
            //定义Mongo服务  
            MongoServer server = MongoServer.Create(connectionString);
            //获取databaseName对应的数据库，不存在则自动创建  
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            try
            {
                if (query == null)
                    return collection.FindAll();
                else
                    return collection.Find(query);
            }
            finally
            {
                server.Disconnect();
            }
        }
        /// <summary>  
        /// 新增  
        /// </summary>   
        public static Boolean Insert(String collectionName, BsonDocument document)
        {
            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            try
            {
                collection.Insert(document);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }
        /// <summary>  
        /// 修改  
        /// </summary>    
        public static Boolean Update(String collectionName, IMongoQuery query, IMongoUpdate new_doc, UpdateFlags flags)
        {
            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            try
            {
                collection.Update(query, new_doc, flags);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }
        /// <summary>  
        /// 移除  
        /// </summary>  
        public static Boolean Remove(String collectionName, IMongoQuery query)
        {
            MongoServer server = MongoServer.Create(connectionString);
            MongoDatabase mongoDatabase = server.GetDatabase(databaseName);
            MongoCollection<BsonDocument> collection = mongoDatabase.GetCollection<BsonDocument>(collectionName);
            try
            {
                collection.Remove(query);
                server.Disconnect();
                return true;
            }
            catch
            {
                server.Disconnect();
                return false;
            }
        }

    }
}
