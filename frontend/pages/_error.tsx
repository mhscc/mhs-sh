import type { NextPage } from 'next';
import Head from 'next/head';

const Error: NextPage = (props: any) => {
  const statusCode: number = props.statusCode ?? 500;
  const is404: boolean = statusCode === 404;

  return (
    <>
      <Head>
        <title>mhs.sh - {statusCode}</title>
      </Head>

      <div className='flex h-screen'>
        <div className='m-auto space-y-8'>
          <div className='text-center divide-gray-700'>
            <h1 className='text-blue-900 text-3xl sm:text-6xl font-bold animate-pulse mb-5'>
              {statusCode}
            </h1>

            <div className='pl-5 space-y-1'>
              <h2 className='text-gray-800 text-2xl font-bold'>
                {is404 ? 'Page not found!' : 'Unknown Error!'}
              </h2>

              <p className='text-sm text-gray-600'>
                {is404
                  ? 'Please check the URL in the address bar and try again.'
                  : 'An unknown error occurred! Please try again later.'}
              </p>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

Error.getInitialProps = ({ res, err }) => {
  const statusCode = res ? res.statusCode : err ? err.statusCode : 404;
  return { statusCode };
};

export default Error;
