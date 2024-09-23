/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        "bg-primary": "var(--bg-primary)",
        "bg-secondary": "var(--bg-secondary)",

        "text-h1": "var(--text-h1)",
        "text-h2": "var(--text-h2)",
        "text-p": "var(--text-p)",

        "btn-bg-primary": "var(--btn-bg-primary)",
        "btn-text-primary": "var(--btn-text-primary)",

        "btn-bg-secondary": "var(--btn-bg-secondary)",
        "btn-text-secondary": "var(--btn-text-secondary)",
      }
    },
  },
  plugins: [],
};