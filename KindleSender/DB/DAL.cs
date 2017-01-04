using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KindleSender
{
    public class DAL
    {
        private DbContext db = new DbContext();

        public void CHeckTableExsit<T>() where T : class
        {
            try
            {
                var type = typeof(T);
                if (!db.CheckTableExist(type.Name))
                {
                    db.CreatTable<T>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmailModel GetModel(Func<EmailModel, bool> where)
        {
            try
            {
                var model = db.GetModel<EmailModel>(where);
                return model;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public EmailModel Edit(EmailModel model)
        {
            try
            {
                if (model.ID == 0)
                {
                    db.Insert<EmailModel>(model);
                    return model;
                }
                else
                {
                    db.Update<EmailModel>(model);
                    return model;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
