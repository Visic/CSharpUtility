using System;

namespace Utility {
    public class Union<T1, T2> {
        //Create methods are just to allow us to create the union with the most derived type, the constructors overload resolution default to the least derived type
        //so if you have Union<object, anything>, the constructor would always default to object
        public static Union<T1, T2> Create(T1 val) {
            return new Union<T1, T2>(val);
        }

        public static Union<T1, T2> Create(T2 val) {
            return new Union<T1, T2>(val);
        }

        protected Union(T1 val) { Type1Value = new Option<T1>(val); }
        protected Union(T2 val) { Type2Value = new Option<T2>(val); }

        public Option<T1> Type1Value { get; private set; }
        public Option<T2> Type2Value { get; private set; }

        public void Match(Action<T1> type1Action, Action<T2> type2Action) {
            if (Type1Value.IsSome) type1Action(Type1Value.Value);
            else type2Action(Type2Value.Value);
        }

        public RT Match<RT>(Func<T1, RT> type1Func, Func<T2, RT> type2Func) {
            return Type1Value.IsSome ? type1Func(Type1Value.Value) : type2Func(Type2Value.Value);
        }

        public Option<RT> Match<RT>(Func<T1, RT> type1Func, Action<T2> type2Action) {
            return Match<Option<RT>>(
                x => new Option<RT>(type1Func(x)),
                x => {
                    type2Action(x);
                    return new Option<RT>();
                }
            );
        }

        public Option<RT> Match<RT>(Action<T1> type1Action, Func<T2, RT> type2Func) {
            return Match<Option<RT>>(
                x => {
                    type1Action(x);
                    return new Option<RT>();
                },
                x => new Option<RT>(type2Func(x))
            );
        }

        public Union<RT1, RT2> Match<RT1, RT2>(Func<T1, RT1> type1Func, Func<T2, RT2> type2Func) {
            return Match<Union<RT1, RT2>>(
                x => Union<RT1, RT2>.Create(type1Func(x)),
                x => Union<RT1, RT2>.Create(type2Func(x))
            );
        }

        public Union<RT1, RT2> Match<RT1, RT2>(Func<T1, RT1> type1Func, Func<T2, Union<RT1, RT2>> type2Func) {
            return Match<Union<RT1, RT2>>(
                x => Union<RT1, RT2>.Create(type1Func(x)),
                x => type2Func(x).Match<Union<RT1, RT2>>(
                    y => Union<RT1, RT2>.Create(y),
                    y => Union<RT1, RT2>.Create(y)
                )
            );
        }

        public Union<RT1, RT2> Match<RT1, RT2>(Func<T1, Union<RT1, RT2>> type1Func, Func<T2, RT2> type2Func) {
            return Match<Union<RT1, RT2>>(
                x => type1Func(x).Match<Union<RT1, RT2>>(
                    y => Union<RT1, RT2>.Create(y),
                    y => Union<RT1, RT2>.Create(y)
                ),
                x => Union<RT1, RT2>.Create(type2Func(x))                
            );
        }

        public override string ToString() {
            return this.Match((object x) => { return x.ToString(); });
        }

        public static implicit operator Union<T1, T2>(Union<T2, T1> val) {
            return val.Match<Union<T1, T2>>(x => Create(x), x => Create(x));
        }

        public static implicit operator Union<T1, T2>(T1 val) {
            return Create(val);
        }

        public static implicit operator Union<T1, T2>(T2 val) {
            return Create(val);
        }
    }

    public static class UnionExtensions {
        public static void Match<T, T1, T2>(this Union<T1, T2> target, Action<T> action) 
            where T1 : T 
            where T2 : T {
                target.Match(
                    t1 => action(t1),
                    t2 => action(t2)
                );
            }

        public static RT Match<RT, T, T1, T2>(this Union<T1, T2> target, Func<T, RT> func) 
            where T1 : T 
            where T2 : T {
                return target.Match<RT>(
                    t1 => func(t1),
                    t2 => func(t2)
                );
            }

        public static Union<T1, T2> Collapse<T1, T2>(this Union<T1, Union<T1, T2>> target) {
            return target.Match<Union<T1, T2>>(
                t1 => Union<T1, T2>.Create(t1),
                t2 => t2
            );
        }

        public static Union<T1, T2> Collapse<T1, T2>(this Union<Union<T1, T2>, T2> target) {
            return target.Match<Union<T1, T2>>(
                t1 => t1,
                t2 => Union<T1, T2>.Create(t2)
            );
        }
    }
}
