using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace ProudSource
{
    public abstract class UserDataAction
    {
		[JsonProperty(PropertyName="action")]
		public string action { get; set; }

        /// <summary>
        /// Abstract method that is not defined here but will be defined in by types but it will essentially Execute the action.
        /// </summary>
        public abstract void execute_action();
    }
}