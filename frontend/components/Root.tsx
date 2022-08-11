import Head from 'next/head';
import Image from 'next/image';
import Footer from '../components/Footer';

const Root = ({ children }: { children: any }) => {
  return (
    <>
      <Head>
        <title>mhs.sh - MHS&apos; Link Shortener</title>
      </Head>

      <div className='flex flex-col h-screen'>
        <div className='flex-grow m-auto pt-32'>
          <div className='flex justify-center'>
            <div className='sm:block hidden'>
              <Image
                src='/images/logo-main.svg'
                alt='Mhs.sh Logo'
                width={267 /*OG: 800*/}
                height={123 /*OG: 370*/}
                draggable={false}
              />
            </div>

            <div className='sm:hidden block'>
              <Image
                src='/images/logo-main.svg'
                alt='Mhs.sh Logo'
                width={200 /*OG: 800*/}
                height={93 /*OG: 370*/}
                draggable={false}
              />
            </div>
          </div>

          {children}
        </div>

        <Footer />
      </div>
    </>
  );
};

export default Root;
