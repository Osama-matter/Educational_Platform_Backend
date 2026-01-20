document.getElementById('loginForm').addEventListener('submit', async function (e) {
    e.preventDefault();

    const email = document.getElementById('email').value;
    const password = document.getElementById('password').value;
    const message = document.getElementById('message');

    try {
        const response = await fetch('https://localhost:7228/api/account/login', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ email, password })
        });

        if (response.ok) {
            const user = await response.json();
            message.textContent = `Welcome, ${user.username}!`;
            message.style.color = 'green';
        } else {
            const errorMessage = await response.text();
            message.textContent = `Login failed: ${errorMessage}`;
            message.style.color = 'red';
        }
    } catch (error) {
        message.textContent = `An error occurred: ${error.message}`;
        message.style.color = 'red';
    }
});
