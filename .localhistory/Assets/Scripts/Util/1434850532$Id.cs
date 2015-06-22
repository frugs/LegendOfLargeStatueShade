using System;
using UnityEditor;

namespace Assets.Scripts.Util {
    public class Id<T> : IEquatable<Id<T>> {
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

        public bool Equals(Id<T> other) {
            if (ReferenceEquals(null, other)) {
                return false;
            }
            if (ReferenceEquals(this, other)) {
                return true;
            }
            return _guid.Equals(other._guid);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) {
                return false;
            }
            if (ReferenceEquals(this, obj)) {
                return true;
            }
            if (obj.GetType() != GetType()) {
                return false;
            }
            return Equals((Id<T>) obj);
        }

        public override int GetHashCode() {
            return _guid.GetHashCode();
        }

        public static bool operator ==(Id<T> left, Id<T> right) {
            return Equals(left, right);
        }

        public static bool operator !=(Id<T> left, Id<T> right) {
            return !Equals(left, right);
        }
    }
}