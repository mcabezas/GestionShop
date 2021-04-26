using System.Collections.Generic;

namespace Libs.Common
{
    public class Context
    {
        private readonly Dictionary<string, string> _queryParams = new Dictionary<string, string>();
        private readonly Dictionary<string, string> _params = new Dictionary<string, string>();
        private string _authorization;
        private string _userId;

        public Context()
        {
        }

        private Context(Context ctx)
        {
            foreach (var param in ctx._params)
            {
                _params.Add(param.Key, param.Key);
            }

            _authorization = ctx._authorization;
            _userId = ctx._userId;
        }

        public string Param(string name)
        {
            return _params.ContainsKey(name) ? _params[name] : "";
        }

        public Context WithAuthorization(string authorization)
        {
            return new Context(this) {_authorization = authorization};
        }

        public Context WithUserId(string userId)
        {
            return new Context(this) {_userId = userId};
        }

        public string Authorization()
        {
            return _authorization;
        }

        public string UserId()
        {
            return _userId;
        }

        public Context WithParams(Dictionary<string, string> parameters)
        {
            var context = new Context(this);
            foreach (var param in parameters)
            {
                context._params.Add(param.Key, param.Value);
            }

            return context;
        }

        public Context WithParam(string name, string value)
        {
            var context = new Context(this);
            context._params.Add(name, value);
            return context;
        }
    }
}