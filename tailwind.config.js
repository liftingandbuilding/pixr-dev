/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{js,jsx,ts,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        'neon-green': '#A3FF00',
        'neon-pink': '#FF9DE0',
        'neon-mint': '#D2FFD2',
        'pixr-dark': '#121212',
        'pixr-gray': '#1a1a1a',
      },
      fontFamily: {
        'pixel': ['"Press Start 2P"', 'monospace'],
        'mono': ['monospace'],
      },
      animation: {
        'pulse-slow': 'pulse 3s ease-in-out infinite',
        'glow': 'glow 2s ease-in-out infinite alternate',
      },
      keyframes: {
        glow: {
          'from': { boxShadow: '0 0 5px #A3FF00' },
          'to': { boxShadow: '0 0 20px #A3FF00' },
        }
      }
    },
  },
  plugins: [],
}
