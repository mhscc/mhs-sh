import classNames from 'classnames';
import Image from 'next/image';

const Footer = () => {
  const footerLinks = [
    { text: 'Source', link: 'https://github.com/mhscc/mhs-sh' },
    { text: 'Contact', link: 'mailto:codingmhs@gmail.com' },
    { text: 'Join', link: 'https://instagram.com/mhs_codingclub' },
    { text: 'Report Abuse', link: 'mailto:roy.dip@mycnmipss.org' }
  ];

  return (
    <footer className='bottom-0 pb-5'>
      <div className='flex justify-center select-none'>
        <div className='sm:block hidden'>
          <Image
            src='/images/mhscc-logo.svg'
            alt='Mhs.sh Logo'
            width={180 /*OG: 450*/}
            height={60 /*OG: 150*/}
            draggable={false}
          />
        </div>

        <div className='sm:hidden block'>
          <Image
            src='/images/mhscc-logo.svg'
            alt='Mhs.sh Logo'
            width={150 /*OG: 450*/}
            height={50 /*OG: 150*/}
            draggable={false}
          />
        </div>
      </div>

      <p className='text-center text-xs text-gray-500'>
        Copyright &copy; MHS Coding Club 2022; all rights are reserved.
      </p>

      <ul className='flex justify-center space-x-2 pt-2'>
        {footerLinks.map(
          ({ text, link }: { text: string; link: string }, i) => (
            <li key={i}>
              <>
                <a
                  className={classNames(
                    'text-blue-500 hover:text-blue-400',
                    'transition ease-in-out delay-100'
                  )}
                  href={link}
                  target='_blank'
                  rel='noreferrer'
                >
                  {text}
                </a>

                {i < footerLinks.length - 1 && (
                  <span className='text-gray-500 select-none pl-2'>•</span>
                )}
              </>
            </li>
          )
        )}
      </ul>
    </footer>
  );
};

export default Footer;
