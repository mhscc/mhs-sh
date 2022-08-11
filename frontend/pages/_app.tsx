import NextNProgress from 'nextjs-progressbar';
import '../styles/globals.css';

const MyApp = ({ Component, pageProps }: any) => (
  <>
    <NextNProgress color='#1E3A8A' />
    <Component {...pageProps} />
  </>
);

export default MyApp;
