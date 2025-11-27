// Replace this with your ngrok URL
export const API_BASE = "https://abcd1234.ngrok.io";

export async function login(email: string, password: string) {
    const res = await fetch(`${API_BASE}/auth/login`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
        credentials: "include"
    });
    return res.json();
}

export async function getMyAnimations() {
    const res = await fetch(`${API_BASE}/animations/my`, {
        method: "GET",
        credentials: "include"
    });
    return res.json();
}

export async function getUserVisuals() {
    const res = await fetch(`${API_BASE}/user-visuals/current`, {
        method: "GET",
        credentials: "include"
    });
    return res.json(); // { backgroundColor: string, animations: string[] }
}