const colors = require('tailwindcss/colors');

module.exports = {
  content: [
    './pages/**/*.{js,ts,jsx,tsx}',
    './components/**/*.{js,ts,jsx,tsx}'
  ],
  theme: {
    colors: {
      transparent: 'transparent',
      current: 'currentColor',
      black: colors.black,
      cyan: colors.cyan,
      white: colors.white,
      gray: colors.neutral,
      green: colors.green,
      emerald: colors.emerald,
      blue: colors.blue,
      indigo: colors.indigo,
      orange: colors.orange,
      pink: colors.pink,
      purple: colors.purple,
      red: colors.red,
      yellow: colors.yellow,
      violet: colors.violet
    },
    extend: {}
  },
  plugins: []
};
