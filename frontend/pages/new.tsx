import axios from 'axios';
import classNames from 'classnames';
import ReCAPTCHA from 'react-google-recaptcha';

import { useRouter } from 'next/router';
import { useState, useRef, useEffect } from 'react';

import { ScissorsIcon, ExclamationIcon } from '@heroicons/react/outline';
import { RadioGroup, Switch } from '@headlessui/react';

import { LoadingAn, LinkInfo, Root } from '../components';

export const getServerSideProps = () => {
  const { PUBLIC_BACKEND_URL, CAPTCHA_PUB_KEY } = process.env;

  return {
    props: { backend: PUBLIC_BACKEND_URL, captchaPubKey: CAPTCHA_PUB_KEY }
  };
};

const NewLinkPage = ({
  backend,
  captchaPubKey
}: {
  backend: string;
  captchaPubKey: string;
}) => {
  const router = useRouter();

  const [expType, setExpType] = useState('Never'),
    [isCustomSlug, setIsCustomSlug] = useState(false),
    [isSubmitLoading, setIsSubmitLoading] = useState(false),
    [captcha, setCaptcha] = useState<string | null>(''),
    [errMsg, setErrMsg] = useState<string | null>(''),
    [storedLinks, setStoredLinks] = useState<ShortLinkInfo[]>([]);

  const urlRef = useRef(null),
    slugRef = useRef(null);

  const expTypes = [
    'Never',
    '24 Hours',
    'One Week',
    'One Month',
    'Three Months',
    'One Year'
  ];

  const submitDataToBackend = async (e: any) => {
    e.preventDefault();

    setIsSubmitLoading(true);
    setErrMsg('');

    const getRefVal: string | any = (ref: any) => {
      const current = ref?.current as any;
      return current === null ? null : current.value;
    };

    const urlRefVal = getRefVal(urlRef),
      slugRefVal = getRefVal(slugRef);

    try {
      const res = await axios.post(backend + '/new', {
        link: urlRefVal,
        slug: slugRefVal,
        exp: expTypes.indexOf(expType),
        captcha
      });

      pushToLocalStorage(res.data as ShortLinkInfo);
      router.reload();
    } catch (e: any) {
      const data = e?.response?.data,
        msg = e?.message;

      if (data) {
        const err = data.error;
        setErrMsg(err);

        if (err == 'Invalid captcha!')
          setTimeout(() => router.reload(), 2 * 1000);
      } else setErrMsg(msg + ' (client-side error)');
    }

    setIsSubmitLoading(false);
  };

  const getFromLocalStorage = () => {
    let data: ShortLinkInfo[] = [];
    const oldData = localStorage.getItem('createdLinks');

    if (typeof oldData === 'string') {
      try {
        data = JSON.parse(oldData) as ShortLinkInfo[];
      } catch {}
    }

    return data.reverse();
  };

  const pushToLocalStorage = (info: ShortLinkInfo) => {
    let data = getFromLocalStorage();

    data.push(info);
    localStorage.setItem('createdLinks', JSON.stringify(data));
  };

  useEffect(() => setStoredLinks(getFromLocalStorage()), [storedLinks]);

  return (
    <Root>
      <>
        <div className='pt-10 space-y-4'>
          <form className='space-y-5' onSubmit={submitDataToBackend}>
            <input
              className={classNames(
                'text-gray-700 w-full p-2 px-3',
                'outline-none rounded-md shadow'
              )}
              type='url'
              name='URL to shorten'
              placeholder='URL to shorten'
              required={true}
              autoComplete='off'
              ref={urlRef}
              disabled={isSubmitLoading}
            />

            <div className='space-y-3'>
              <Switch.Group as='div' className='flex items-center'>
                <Switch
                  checked={isCustomSlug}
                  onChange={setIsCustomSlug}
                  className={classNames(
                    isCustomSlug ? 'bg-blue-800' : 'bg-gray-200',
                    'relative inline-flex flex-shrink-0 h-6 w-11',
                    'border-2 border-transparent rounded-full cursor-pointer',
                    'transition-colors ease-in-out duration-100 focus:outline-none'
                  )}
                  disabled={isSubmitLoading}
                >
                  <span
                    aria-hidden='true'
                    className={classNames(
                      isCustomSlug ? 'translate-x-5' : 'translate-x-0',
                      'pointer-events-none inline-block h-5 w-5 rounded-full',
                      'bg-white shadow transform ring-0',
                      'transition ease-in-out duration-100'
                    )}
                  />
                </Switch>

                <Switch.Label as='span' className='ml-3'>
                  <span className='text-sm font-medium text-gray-900'>
                    Custom slug
                  </span>
                </Switch.Label>
              </Switch.Group>

              {isCustomSlug && (
                <div className='space-y-1'>
                  <div className='flex rounded-md shadow'>
                    <span
                      className={classNames(
                        'inline-flex items-center px-3 rounded-l-md',
                        'bg-gray-50 text-gray-700 sm:text-sm shadow'
                      )}
                    >
                      mhs.sh/
                    </span>

                    <input
                      type='text'
                      name='company-website'
                      id='company-website'
                      className={classNames(
                        'text-gray-700 p-2 px-3 outline-none',
                        'rounded-md rounded-l-none shadow w-full'
                      )}
                      placeholder='e.g. SomeCustomSlug'
                      required={true}
                      autoComplete='off'
                      ref={slugRef}
                      disabled={isSubmitLoading}
                    />
                  </div>

                  <p className='text-sm text-gray-700'>
                    Three (3) characters minimum; 32 characters maximum.
                  </p>
                </div>
              )}
            </div>

            <div>
              <span className='font-medium'>Expiration:</span>

              <RadioGroup
                value={expType}
                onChange={setExpType}
                className='mt-2'
              >
                <div className='grid grid-cols-3 gap-3'>
                  {expTypes.map((e, i) => (
                    <RadioGroup.Option
                      key={i}
                      value={e}
                      className={({ checked }) =>
                        classNames(
                          checked
                            ? 'bg-blue-800 border-transparent text-white hover:bg-blue-700'
                            : 'bg-white border-gray-200 text-gray-900 hover:bg-gray-50',
                          'border rounded-md py-2 px-3 flex items-center',
                          'justify-center text-sm font-medium sm:flex-1 shadow',
                          'transition ease-in-out delay-100'
                        )
                      }
                      disabled={isSubmitLoading}
                    >
                      <RadioGroup.Label as='span'>{e}</RadioGroup.Label>
                    </RadioGroup.Option>
                  ))}
                </div>
              </RadioGroup>
            </div>

            <div className='flex justify-center'>
              <ReCAPTCHA
                sitekey={captchaPubKey}
                onChange={i => setCaptcha(i)}
                onExpired={() => router.reload()}
              />
            </div>

            <button
              className={classNames(
                'bg-blue-900 hover:bg-blue-800 text-white',
                'p-2 px-5 w-full',
                'rounded-md shadow',
                'hover:shadow-md active:shadow-none',
                'transition ease-in-out delay-100'
              )}
              type='submit'
              disabled={isSubmitLoading}
            >
              <div className='flex justify-center space-x-1'>
                {isSubmitLoading ? (
                  <LoadingAn />
                ) : (
                  <>
                    <ScissorsIcon className='h-5 w-5' />
                    <span>Shorten!</span>
                  </>
                )}
              </div>
            </button>
          </form>

          {errMsg !== '' && (
            <div className='grid grid-cols-7 items-center bg-red-500 rounded p-5 w-96'>
              <ExclamationIcon className='text-red-300 w-8 h-8' />
              <p className='col-span-6 text-white select-text'>{errMsg}</p>
            </div>
          )}
        </div>

        {storedLinks.length > 0 && (
          <div className='pt-5 pb-12'>
            <div className='flex space-x-2'>
              <span className='text-xl font-medium'>My Links</span>

              <span className='bg-blue-800 text-white p-0.5 px-2 rounded'>
                {storedLinks.length}
              </span>
            </div>

            <ul className='space-y-2 pt-5'>
              {storedLinks.map((e: ShortLinkInfo, i) => (
                <li key={i}>
                  <LinkInfo info={e} />
                </li>
              ))}
            </ul>
          </div>
        )}
      </>
    </Root>
  );
};

export default NewLinkPage;
