using System;

namespace MauronAlpha.Events.Units
{
    public class EventUnit_subscription:EventComponent_unit {

        public EventUnit_subscription(
            string code,
            EventHandler.DELEGATE_condition condition,
            EventHandler.DELEGATE_trigger trigger,
            int executionCount
        ):base() {
            Condition = condition;
            Trigger = trigger;
        }

        EventHandler.DELEGATE_condition Condition;
        EventHandler.DELEGATE_trigger Trigger;
    }
}
