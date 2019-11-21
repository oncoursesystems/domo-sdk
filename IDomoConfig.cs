using System;

namespace OnCourse.Domo.Sdk
{
    public interface IDomoConfig
    {
        /// <summary>
        /// Domo App Client Id for Auth
        /// </summary>
		string ClientId { get; set; }

        /// <summary>
        /// Domo App Secret for Auth
        /// </summary>
		string ClientSecret { get; set; }

        /// <summary>
        /// Base URI for api
        /// </summary>
		Uri ApiHost { get; set; }

        /// <summary>
        /// Scope for Authorization. Data and/or Users scopes
        /// </summary>
		DomoAuthScope Scope { get; set; }
    }
}