using System;

namespace Utility {
    public class Union<T1, T2> {
        Some<T1>? _type1Value;
        Some<T2>? _type2Value;

        protected Union() { }
        public Union(T1 val) { _type1Value = val; }
        public Union(T2 val) { _type2Value = val; }

        public void Match(Action<T1> type1Action, Action<T2> type2Action) {
            if (_type1Value != null) type1Action(_type1Value.Value);
            else type2Action(_type2Value.Value);
        }

        public RT Match<RT>(Func<T1, RT> type1Func, Func<T2, RT> type2Func) {
            return _type1Value != null ? type1Func(_type1Value.Value) : type2Func(_type2Value.Value);
        }

        public Option<RT> Match<RT>(Func<T1, RT> type1Func, Action<T2> type2Action) {
            return Match<Option<RT>>(
                x => type1Func(x),
                x => {
                    type2Action(x);
                    return Option.New<RT>();
                }
            );
        }

        public Option<RT> Match<RT>(Action<T1> type1Action, Func<T2, RT> type2Func) {
            return Match<Option<RT>>(
                x => {
                    type1Action(x);
                    return Option.New<RT>();
                },
                x => type2Func(x)
            );
        }

        public Union<RT1, RT2> Match<RT1, RT2>(Func<T1, RT1> type1Func, Func<T2, RT2> type2Func) {
            return Match<Union<RT1, RT2>>(
                x => new Union<RT1, RT2>(type1Func(x)),
                x => new Union<RT1, RT2>(type2Func(x))
            );
        }

        public Union<RT1, RT2> Match<RT1, RT2>(Func<T1, RT1> type1Func, Func<T2, Union<RT1, RT2>> type2Func) {
            return Match<Union<RT1, RT2>>(
                x => new Union<RT1, RT2>(type1Func(x)),
                x => type2Func(x).Match<Union<RT1, RT2>>(
                    y => new Union<RT1, RT2>(y),
                    y => new Union<RT1, RT2>(y)
                )
            );
        }

        public Union<RT1, RT2> Match<RT1, RT2>(Func<T1, Union<RT1, RT2>> type1Func, Func<T2, RT2> type2Func) {
            return Match<Union<RT1, RT2>>(
                x => type1Func(x).Match<Union<RT1, RT2>>(
                    y => new Union<RT1, RT2>(y),
                    y => new Union<RT1, RT2>(y)
                ),
                x => new Union<RT1, RT2>(type2Func(x))                
            );
        }

        public override string ToString() {
            return this.Match((object x) => { return x.ToString(); });
        }

        public static implicit operator Union<T1, T2>(Union<T2, T1> val) {
            return val.Match<Union<T1, T2>>(x => new Union<T1, T2>(x), x => new Union<T1, T2>(x));
        }

        public static implicit operator Union<T1, T2>(T1 val) {
            return new Union<T1, T2>(val);
        }

        public static implicit operator Union<T1, T2>(T2 val) {
            return new Union<T1, T2>(val);
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
                t1 => new Union<T1, T2>(t1),
                t2 => t2
            );
        }

        public static Union<T1, T2> Collapse<T1, T2>(this Union<Union<T1, T2>, T2> target) {
            return target.Match<Union<T1, T2>>(
                t1 => t1,
                t2 => new Union<T1, T2>(t2)
            );
        }
    }
}
