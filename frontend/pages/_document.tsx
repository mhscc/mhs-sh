import Document, { Html, Head, Main, NextScript } from 'next/document';

class MyDocument extends Document {
  static async getInitialProps(ctx: any) {
    const initialProps = await Document.getInitialProps(ctx);
    return { ...initialProps };
  }

  render() {
    const dec = "mhs.sh - MHS' Link Shortener";
    const url = 'https://app.mhs.sh';

    return (
      <Html lang='en-US'>
        <Head>
          <meta charSet='utf-8' />
          <meta name='robots' content='noindex, nofollow' />

          <meta httpEquiv='Content-Type' content='text/html; charset=UTF-8' />
          <meta httpEquiv='Content-Script-Type' content='text/javascript' />

          <meta name='description' content={dec} />
          <meta name='og:description' content={dec} />

          <link rel='canonical' href={url}></link>
          <meta name='og:url' content={url} />

          <meta property='og:site_name' content={url.replace('https://', '')} />
          <meta property='og:type' content='website' />

          <meta
            name='keywords'
            content='mhs.sh, app.mhs.sh, mhs-short, link shortener'
          />

          <link
            rel='apple-touch-icon'
            sizes='180x180'
            href='/favicons/apple-touch-icon.png'
          />

          <link
            rel='icon'
            type='image/png'
            sizes='32x32'
            href='/favicons/favicon-32x32.png'
          />

          <link
            rel='icon'
            type='image/png'
            sizes='16x16'
            href='/favicons/favicon-16x16.png'
          />

          <link rel='manifest' href='/favicons/site.webmanifest' />

          <link
            rel='mask-icon'
            href='/favicons/safari-pinned-tab.svg'
            color='#1e3a8a'
          />

          <link rel='shortcut icon' href='/favicons/favicon.ico' />
          <meta name='msapplication-TileColor' content='#1e3a8a' />

          <meta
            name='msapplication-config'
            content='/favicons/browserconfig.xml'
          />

          <meta name='theme-color' content='#ffffff' />
        </Head>

        <noscript
          style={{ color: 'red', fontSize: '28px', fontWeight: 'bold' }}
        >
          Please enable JavaScript!
        </noscript>

        <body className='bg-gray-100 select-none'>
          <Main />
          <NextScript />
        </body>
      </Html>
    );
  }
}

export default MyDocument;
