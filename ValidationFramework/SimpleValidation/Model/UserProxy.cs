using SimpleValidation.Validation;
using System;

namespace SimpleValidation.Model
{
    public class UserProxy : EntityProxy
    {
        public readonly static string ATTRIBUTE_NAME = "name";
        public readonly static string ATTRIBUTE_ID = "id";

        private User user;

        public UserProxy(User user)
        {
            this.user = user;
        }

        protected override object GetAttribute(string attribute)
        {
            if (attribute.ToLower().Equals(ATTRIBUTE_NAME))
                return user.Name;
            else if (attribute.ToLower().Equals(ATTRIBUTE_ID))
                return user.ID;
            // else if ...

            return null;
        }

        protected override void SetAttribute(string attribute, object value)
        {
            if (attribute.ToLower().Equals(ATTRIBUTE_NAME))
                user.Name = (string)value;
            else if (attribute.ToLower().Equals(ATTRIBUTE_ID))
                user.ID = int.Parse(value.ToString());
            // else if ...

        }

        public string GetName()
        {
            return user.Name;
        }

        public Reporter SetName(string name)
        {
            return CheckThenSetAttribute(ATTRIBUTE_NAME, name);
        }

        public int GetID()
        {
            return user.ID;
        }

        public Reporter SetID(int id)
        {
            return CheckThenSetAttribute(ATTRIBUTE_ID, id);
        }
    }
}
