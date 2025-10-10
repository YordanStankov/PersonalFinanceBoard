import { type User } from '../Models/User';



function Profile() {
   /* const [profile, setProfile] = useState<User | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetch('https://localhost:7010/api/User')
            .then((res) => {
                if (!res.ok) throw new Error('Failed to fetch profile');
                return res.json();
            })
            .then((data) => {
                setProfile(data);
                setLoading(false);
            })
            .catch((err) => {
                setError(err.message);
                setLoading(false);
            });
    }, []);

    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {error}</div>;
    if (!profile) return <div>No profile data found.</div>;

*/ const profile: User = { id: 'whawhatwhatwhatwaht', userName: 'JohnDoe', email: 'manthisfuckinglangugesucksman@gmail.com'}
    return (
        <div className="profile-container">
            <h2>{profile.userName}</h2>
            <p>Email: {profile.email}</p>
        </div>
    );
}

export default Profile;

