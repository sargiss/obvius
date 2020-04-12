using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Html;
using Nop.Services.Plugins;
using Nop.Services.Cms;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Product.VideoUploader
{
    public class VideoUploaderPlugin: BasePlugin, IWidgetPlugin
    {
        static VideoUploaderPlugin()
        {
            LinqToDB.Data.DataConnection.TurnTraceSwitchOn();
            LinqToDB.Data.DataConnection.WriteTraceLine = (message, displayName) => { 
                Console.WriteLine($"{message} {displayName}"); 
            };
        }

        /// <summary>
        /// Gets a value indicating whether to hide this plugin on the widget list page in the admin area
        /// </summary>
        public bool HideInWidgetList => false;

        /// <summary>
        /// Gets widget zones where this widget should be rendered
        /// </summary>
        /// <returns>Widget zones</returns>
        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "VideoUploaderWidget";
        }

        /// <summary>
        /// Gets a name of a view component for displaying widget
        /// </summary>
        /// <param name="widgetZone">Name of the widget zone</param>
        /// <returns>View component name</returns>
        public IList<string> GetWidgetZones()
        {
            return new List<string> { AdminWidgetZones.ProductDetailsBlock };
        }

        public override void Install()
        {
            //Logic during installation goes here...

            base.Install();
        }

        public override void Uninstall()
        {
            //Logic during uninstallation goes here...

            base.Uninstall();
        }
    }
}
