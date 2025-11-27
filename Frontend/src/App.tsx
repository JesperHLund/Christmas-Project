import { useEffect, useState } from "react";
import { login, getMyAnimations, getUserVisuals } from "./api";
import Lottie from "react-lottie-player";

function App() {
  // Login form state
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  // Visual and animation state
  const [backgroundColor, setBackgroundColor] = useState("#007BFF"); // temporary default
  const [animations, setAnimations] = useState<any[]>([]);

  // Indicates if user is logged in
  const [isLoggedIn, setIsLoggedIn] = useState(false);

  // Store loaded animation JSONs
  const [loadedAnimations, setLoadedAnimations] = useState<{ [key: string]: any }>({});

  // Handle login
  const handleLogin = async () => {
    const result = await login(email, password);
    console.log(result);

    if (!result.message.includes("Logged in")) return;

    setIsLoggedIn(true);
    await fetchUserVisuals();
    await fetchAnimations();
  };

  // Fetch animations associated with the user
  const fetchAnimations = async () => {
    const data = await getMyAnimations();
    setAnimations(data);

    // Dynamically load animation JSONs
    const loaded: { [key: string]: any } = {};
    await Promise.all(
      data.map(async (a: any) => {
        try {
          const module = await import(`./data/${a.fileName}`);
          loaded[a.fileName] = module.default;
        } catch (err) {
          console.error("Failed to load animation:", a.fileName, err);
        }
      })
    );
    setLoadedAnimations(loaded);
  };

  // Fetch server-driven visual settings (background color, animation files)
  const fetchUserVisuals = async () => {
    const visuals = await getUserVisuals();
    if (visuals.backgroundColor) setBackgroundColor(visuals.backgroundColor);
    if (visuals.animations) setAnimations(visuals.animations);

    // Load visuals JSON as well
    const loaded: { [key: string]: any } = {};
    if (visuals.animations) {
      await Promise.all(
        visuals.animations.map(async (a: any) => {
          try {
            const module = await import(`./data/${a.fileName}`);
            loaded[a.fileName] = module.default;
          } catch (err) {
            console.error("Failed to load animation:", a.fileName, err);
          }
        })
      );
      setLoadedAnimations(loaded);
    }
  };

  // Optionally fetch visuals and animations on mount if user is already logged in
  useEffect(() => {
    if (isLoggedIn) {
      fetchUserVisuals();
      fetchAnimations();
    }
  }, [isLoggedIn]);

  return (
    <div style={{ backgroundColor: backgroundColor, minHeight: "100vh", padding: "1rem" }}>
      <h1>Christmas Animations</h1>

      {!isLoggedIn && (
        <div style={{ marginBottom: "2rem" }}>
          <input
            type="email"
            value={email}
            onChange={e => setEmail(e.target.value)}
            placeholder="Email"
            style={{ display: "block", margin: "0.5rem 0" }}
          />
          <input
            type="password"
            value={password}
            onChange={e => setPassword(e.target.value)}
            placeholder="Password"
            style={{ display: "block", margin: "0.5rem 0" }}
          />
          <button onClick={handleLogin}>Login</button>
        </div>
      )}

      {isLoggedIn && (
        <>
          <h2>Your Animations</h2>
          <div>
            {animations.map((a: any, index: number) => {
              const animationData = loadedAnimations[a.fileName];
              if (!animationData) return null;

              return (
                <div key={index} style={{ margin: "1rem 0" }}>
                  <Lottie
                    loop
                    play
                    animationData={animationData}
                    style={{ width: 300, height: 300 }}
                  />
                </div>
              );
            })}
          </div>
        </>
      )}
    </div>
  );
}

export default App;
