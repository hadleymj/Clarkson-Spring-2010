function result = diffSteps(data, col, thres)

[rows, cols] = size(data);
temp = [0];
%for i = 1:(rows-1)
%   result(i,:) = data(i+1, :) - data(i, :);
%end

%data = abs(data);
%for i = 1:rows-2
%    result(i,:) = (data(i,:) + data(i+1,:) + data(i+2,:))/3;
%end

for i = 1:rows-4
    if data(i,col) > thres && data(i+4,col) > thres && ( data(i+1,col) < -1*thres || data(i+2,col) < -1*thres || data(i+3,col) < -1*thres)
        max_cur = max(data(i,col), data(i+4,col));
        if ( data(i+1,col) < max_cur )
            data(i+1,col) = max_cur;
        end
        if ( data(i+2,col) < max_cur )
            data(i+2,col) = max_cur;
        end
        if ( data(i+3,col) < max_cur )
            data(i+3,col) = max_cur;
        end
    end
    temp(i) = data(i,col);
    if ( temp(i) < 0 )
        temp(i) = 0;
    end
    
end

result = [0];

for i = 1:size(temp')-4
    if temp(i) >= thres/2 && temp(i+4) >= thres/2 && ( temp(i+1) < thres/2  || temp(i+2) < thres/2 || temp(i+3) < thres/2)
        max_cur = max(temp(i), temp(i+4));
        if ( temp(i+1) < max_cur )
            temp(i+1) = max_cur;
        end
        if ( temp(i+2) < max_cur )
            temp(i+2) = max_cur;
        end
        if ( temp(i+3) < max_cur )
            temp(i+3) = max_cur;
        end
    end
    result(i) = temp(i);
    if ( result(i) < thres/2 )
        result(i) = 0;
    end
    
end


        
        