using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Validation;
using RetireHappy.Models;

namespace RetireHappy.DAL
{
    public class CommonGateway<T> : ICommonGateway<T> where T : class
    {
        internal RetireHappyContext db = new RetireHappyContext();
        internal DbSet<T> data = null;

        public CommonGateway()
        {
            data = db.Set<T>();
        }

        public T Delete(int? id)
        {
            T obj = SelectById(id);
            data.Remove(obj);
            Save();
            return obj;
        }

        public void Insert(T obj)
        {
            data.Add(obj);
            Save();
        }

        public void Save()
        {
            try
            {
                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Console.WriteLine("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public IEnumerable<T> SelectAll()
        {
            return data.ToList();
        }

        public void Update(T obj)
        {
            db.Entry(obj).State = System.Data.Entity.EntityState.Modified;
            Save();
        }

        public T SelectById(int? id)
        {
            T obj = data.Find(id);
            return obj;
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}