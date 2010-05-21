
%As the data is received feed it into this function 1 sample at a time.
%There is a .125 second delay between the user begining to take a step
% and the system recognizing the user is taking a step.
function ProcessRawData(data, offset, thres, reset)

persistent buff;
persistent zero_count;

%If the function is being reset.
if reset == 1
    buff = zeros(5,3);
    zero_count = 0;
    StepEventTrigger(1);
    return;
end

data = data - offset;

%Otherwise put the new data in the last slot in the buffer.
buff(5,:) = data;


%First processing cycle on the five samples.
%This attempts to take periods of shifting positive negative forces
%and normalize to all positive. 
%If the accel is less than zero it is set to zero.
if buff(1,3) > thres && buff(5,3) > thres && ( buff(2,3) < -1*thres || buff(3,3) < -1*thres || buff(4,3) < -1*thres )
    max_cur = max(buff(1,3), buff(5,3));
    if ( buff(2,3) < max_cur )
        buff(2,3) = max_cur;
    end
    if ( buff(3,3) < max_cur )
        buff(3,3) = max_cur;
    end
    if ( buff(4,3) < max_cur )
        buff(4,3) = max_cur;
    end
end

if ( buff(1,3) < 0 )
    buff(1,3) = 0;
end

%Second processing cycle on the five samples.
%Similar to the first but attempts to remove areas where the accel dropped
%below the threshold/2 but is surrounded on either side by values above the
%threshold.
if buff(1,3) >= thres/2 && buff(5,3) >= thres/2 && ( buff(2,3) < thres/2  || buff(3,3) < thres/2 || buff(4,3) < thres/2)
    max_cur = max(buff(1,3), buff(5,3));
    if ( buff(2,3) < max_cur )
        buff(2,3) = max_cur;
    end
    if ( buff(3,3) < max_cur )
        buff(3,3) = max_cur;
    end
    if ( buff(4,3) < max_cur )
        buff(4,3) = max_cur;
    end
end

%If the buff point is a value below half the threshold it is set to zero.
if ( buff(1,3) < thres/2 )
    buff(1,3) = 0;
end


%If there are five zero readings in a row count the next positive reading
% as a setp.
if zero_count >= 5 && buff(1,3) > 0
    StepEventTrigger(0);
    zero_count = 0;
end

if ( buff(1,3) == 0 )
    zero_count = zero_count + 1;
end

%Pop the first element off of the Array and move all the elements
%up one cell.
for n = 1:4
    buff(n,:) = buff(n+1,:);
end


end

%The ProcessRawData function will call this whenever a step begins. This 
%is a quick function that should process the fact that a step is occurring
%very quickly.
function StepEventTrigger(reset)
persistent step_count;

if reset == 1
    step_count = 1;
else
    step_count = step_count + 1;
end

fprintf('Step Count: %i\n', step_count);

end


