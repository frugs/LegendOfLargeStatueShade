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
    }
}