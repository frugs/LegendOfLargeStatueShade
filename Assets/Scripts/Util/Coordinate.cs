namespace Assets.Scripts.Util {
    public class Coordinate {

        private readonly int _x;
        private readonly int _y;

        public int X {
            get { return _x; }
        }

        public int Y {
            get { return _y; }
        }

        public Coordinate North {
            get { return new Coordinate(_x, _y + 1); }
        }

        public Coordinate East {
            get { return new Coordinate(_x - 1, _y); }
        }

        public Coordinate South {
            get { return new Coordinate(_x, _y - 1); }
        }

        public Coordinate West {
            get { return new Coordinate(_x + 1, _y); }
        }

        public Coordinate(int x, int y) {
            _x = x;
            _y = y;
        }

        protected bool Equals(Coordinate other) {
            return _x == other._x && _y == other._y;
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
            return Equals((Coordinate) obj);
        }

        public override int GetHashCode() {
            unchecked {
                return (_x * 397) ^ _y;
            }
        }
    }
}
