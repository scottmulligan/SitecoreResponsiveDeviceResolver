#region

using Sitecore;
using Sitecore.Rules;
using Sitecore.Rules.Conditions;

#endregion

namespace ResponsiveDeviceResolver.Rules.Conditions
{
    public class ContextItemHasLayoutDetailsForThisDevice<T> :
        OperatorCondition<T>
        where T : RuleContext
    {
        public string DeviceId { get; set; }

        protected override bool Execute(T ruleContext)
        {
            var deviceItem = Context.Database.GetItem(DeviceId);
            if (deviceItem == null) return false;
            return Context.Item.Visualization.GetLayout(deviceItem) != null;
        }
    }
}