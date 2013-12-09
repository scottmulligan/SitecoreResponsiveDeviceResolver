#region

using Sitecore;
using Sitecore.Data.Items;
using Sitecore.Rules;
using Sitecore.Rules.Actions;

#endregion

namespace ResponsiveDeviceResolver.Rules.Actions
{
    public class SetContextDevice<T> :
        RuleAction<T>
        where T : RuleContext
    {
        public string DeviceID { get; set; }

        public override void Apply(T ruleContext)
        {
            Context.Device = new DeviceItem(
                ruleContext.Item.Database.GetItem(DeviceID));
            ruleContext.Abort();
        }
    }
}