#region

using System;
using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Pipelines.HttpRequest;
using Sitecore.Rules;

#endregion

namespace ResponsiveDeviceResolver.Pipelines.HttpRequest
{
    public class ResponsiveDeviceResolver : HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            if (Context.Database == null
                || Context.Item == null
                || Sitecore.Context.PageMode.IsPageEditorEditing
                || String.Compare(Context.Database.Name, "core", StringComparison.OrdinalIgnoreCase) == 0)
            {
                return;
            }

            RunModuleRules();
        }

        /// <summary>
        ///     Invoke global device resolution rules.
        /// </summary>
        /// <returns>True if a global device resolution rule applied.</returns>
        protected bool RunModuleRules()
        {
            Item moduleRuleItem = Context.Database.GetItem("{143624D2-7C7F-460A-B97E-068283D646B9}");
            if (moduleRuleItem == null)
                return false;

            string ruleXml = moduleRuleItem["Rule"];

            if (String.IsNullOrEmpty(ruleXml) || moduleRuleItem["Disable"] == "1")
                return false;

            // parse the rule XML
            RuleList<RuleContext> rules = new RuleList<RuleContext> {Name = moduleRuleItem.Paths.Path};
            RuleList<RuleContext> parsed = RuleFactory.ParseRules<RuleContext>(
                Context.Database,
                ruleXml);
            rules.AddRange(parsed.Rules);

            if (rules.Count < 1)
                return false;

            // invoke the rule
            RuleContext ruleContext = new RuleContext {Item = Context.Item};
            rules.Run(ruleContext);

            // rule applied
            return ruleContext.IsAborted;
        }
    }
}