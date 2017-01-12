using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Valeo.Domain;
using PetaPoco;
using Valeo.Common;


namespace Valeo.Service
{
    public class SetEmailService : BaseService
    {

        public SetEmailModel GetModel()
        {
            var userModel = db.FirstOrDefault<SetEmailModel>("select  * from dbo.m_SetEmail");
            if (userModel == null)
            {
                userModel = new SetEmailModel();
            }
            else
            {
                //var tmpdec = "";
                //try
                //{
                //    //tmpdec = Encryption.Decode(userModel.MailPassword);
                //    //userModel.MailPassword = tmpdec;
                //}
                //catch (Exception )
                //{
                //}
            }

            return userModel;

        }
        public void Add(SetEmailModel model)
        {
            //model.MailPassword = Encryption.Encode(model.MailPassword);
            db.Insert(model);
        }
        public void Edit(SetEmailModel model)
        {
            //model.MailPassword = Encryption.Encode(model.MailPassword);
            db.Update(model);
        }
    }
}
