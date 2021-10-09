using System;
namespace Yatter.Invigoration.Azure.TActor
{
    public static class Extensions
    {
        public static ActionBase AddTObjectToTActor(this ActionBase tActor, ObjectBase tObject)
        {
            tActor.AddObject(tObject);

            return tActor;
        }

        public static TActor AddTObjectToTActor<TActor>(this ActionBase tActor, ObjectBase tObject) where TActor : ActionBase, new()
        {
            tActor.AddObject(tObject);

            return (TActor)tActor;
        }
    }
}

