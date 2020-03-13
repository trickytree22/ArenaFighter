using System;
using System.Collections.Generic;
using System.Text;

namespace ArenaFighter
{
    class Items
    {
        internal bool IsMultiUse; //potion or such with multiple doses
        internal bool IsActive; //once equipped or used, check whether bonus in effect
        internal bool IsPersistent; //as opposed to temporary, which should be measured in rounds or battles

    }
}
