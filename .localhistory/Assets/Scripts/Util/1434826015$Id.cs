using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEditor;

namespace Assets.Scripts.Util {
    public class Id<T> {
        private readonly GUID _guid;

        public Id(GUID guid) {
            _guid = guid;
        }

        public GUID ToGuid() {
            return _guid;
        }

        public override string ToString() {
            return _guid.ToString();
        }

        private sealed class GuidEqualityComparer : IEqualityComparer<Id<T>> {
            public bool Equals(Id<T> x, Id<T> y) {
                if (ReferenceEquals(x, y)) {
                    return true;
                }
                if (ReferenceEquals(x, null)) {
                    return false;
                }
                if (ReferenceEquals(y, null)) {
                    return false;
                }
                if (x.GetType() != y.GetType()) {
                    return false;
                }
                return x._guid.Equals(y._guid);
            }

            public int GetHashCode(Id<T> obj) {
                return obj._guid.GetHashCode();
            }
        }

        private static readonly IEqualityComparer<Id<T>> GuidComparerInstance = new GuidEqualityComparer();

        public static IEqualityComparer<Id<T>> GuidComparer {
            get { return GuidComparerInstance; }
        }
    }
}