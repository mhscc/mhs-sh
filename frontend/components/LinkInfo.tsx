import { ClipboardCopyIcon, ExternalLinkIcon } from '@heroicons/react/outline';
import classNames from 'classnames';

const LinkInfo = ({ info }: { info: ShortLinkInfo }) => {
  const { link, slug } = info;

  const linkClassnames = classNames(
    'text-blue-500 hover:text-blue-400',
    'transition ease-in-out delay-100'
  );

  const getQrCodeUrl = (size: number) =>
    `https://api.qrserver.com/v1/create-qr-code?size=${size}x${size}&format=png&data=https://mhs.sh/${slug}`;

  const truncate = (str: string | undefined, num: number = 7) => {
    if (str === undefined || str === null) return 'NULL';

    const asStr = str.toString();

    return asStr.length <= num ? asStr : asStr.slice(0, num) + '...';
  };

  return (
    <div className='flex items-center space-x-4 bg-white shadow rounded p-4 w-96'>
      <div className='relative z-0 select-none'>
        <img
          className='flex justify-center items-center'
          src={getQrCodeUrl(65)}
          alt='QR Code'
        />

        <div className='absolute inset-0 flex justify-center items-center z-10'>
          <a
            className={classNames(
              'bg-transparent hover:bg-black hover:bg-opacity-50',
              'transition ease-in-out delay-100'
            )}
            href={getQrCodeUrl(250)}
            target='_blank'
            rel='noreferrer'
          >
            <ExternalLinkIcon
              className={classNames(
                'text-transparent hover:text-white w-16 h-16 p-4',
                'transition ease-in-out delay-100'
              )}
            />
          </a>
        </div>
      </div>

      <div>
        <div className='flex space-x-1 items-center'>
          <a
            className={classNames(
              linkClassnames,
              'font-medium text-lg select-text'
            )}
            href={'https://mhs.sh/' + slug}
            target='_blank'
            rel='noreferrer'
          >
            mhs.sh/{slug}
          </a>

          <button
            className='hover:bg-gray-200 rounded p-1'
            type='button'
            onClick={() => {
              try {
                navigator.clipboard.writeText('https://mhs.sh/' + slug);
              } catch {}
            }}
          >
            <ClipboardCopyIcon className='text-gray-500 w-6 h-6' />
          </button>
        </div>

        <a
          className={classNames(linkClassnames, 'text-xs')}
          href={link}
          target='_blank'
          rel='noreferrer'
        >
          {truncate(link, 32)}
        </a>
      </div>
    </div>
  );
};

export default LinkInfo;
